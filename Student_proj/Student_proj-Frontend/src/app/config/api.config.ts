import { envirnoment } from "../../Environments/Enviroment";

export const api_config = {
    base_url:envirnoment.api_url,
    endpoints:{
        auth:{
            login:'api/Auth/Login',
            register:'api/Auth/register',
            forgot_Password:(email:string)=>`api/Auth/forgot-password/${email}`,
            reset_Password:(email:string)=>`api/Auth/reset-password/${email}`,
            refresh:'api/Auth/refresh',
            logout:'api/Auth/logout'
        },
        students:{
            getAll:'api/Student',
            getPaginated: 'api/Student/paginated',
            create:'api/Student',
            getById:(id:number)=>`api/Student/${id}`,
            update:(id:number)=>`api/Student/${id}`,
            delete:(id:number)=>`api/Student/${id}`,
            getByName:(name:string)=>`api/Student/by-name/${name}`,
            upsert:'api/Student/upsert'
        },
        dashboard: {
            getStats: 'api/Dashboard/stats'
        }
    }
}