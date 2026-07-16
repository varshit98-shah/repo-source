import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../features/auth/services/auth.service';

import { catchError, switchMap } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  let clonedReq = req;
  if (token) {
    clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  return next(clonedReq).pipe(
    catchError((error) => {
      // If error is 401 and not a refresh token call itself
      if (error.status === 401 && !req.url.includes('/Auth/refresh')) {
        const refreshToken = sessionStorage.getItem('refresh_token');
        if (token && refreshToken) {
          return authService.refreshToken({ accessToken: token, refreshToken }).pipe(
            switchMap((res: any) => {
              if (res.data && res.data.token) {
                const newReq = req.clone({
                  setHeaders: {
                    Authorization: `Bearer ${res.data.token}`
                  }
                });
                return next(newReq);
              }
              authService.logout();
              return throwError(() => error);
            }),
            catchError((refreshErr) => {
              authService.logout();
              return throwError(() => refreshErr);
            })
          );
        } else {
          authService.logout();
        }
      }
      return throwError(() => error);
    })
  );
};
