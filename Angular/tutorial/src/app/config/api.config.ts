import { enviroment } from "../Enviroment/Enviroment";

export const apiconfig={
    BASE_URL :enviroment.apiUrl,
    ENDPOINTS:{
        AUTH:{
            LOGIN :"/api/Auth/login",
            REGISTER:"/api/Auth/Register"
        }
    }
}