import axiosClient from "./axiosClient";


// export interface LoginResponse {
//     success: boolean;
//     message: string;
//     data: LoginData;
// }

// export interface LoginData {
//     maTK: number;
//     tenHienThi: string;
//     token: string;
//     expireIn: number;
// }


// export async function login(
//     tenTK: string,
//     matKhau: string
// ): Promise<LoginData> {
//     const res = await fetch(
//         'http://localhost:5196/api/Auth/Login',
//         {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({ tenTK, matKhau }),
//         }
//     );

//     const json: LoginResponse = await res.json();
//     if (!res.ok || !json.success)
//         throw new Error(json.message || 'Login failed');

//     return json.data;
// }


//Fetch

// export interface RegisterResponse {

//     success: boolean;
//     message: string;
// }


// export interface RegisterData {
//     tenTK: string,
//     matKhau: string,
//     tenHienThi: string,
//     email: string,
//     phone: string,
// }

// export interface RegisterFormData extends RegisterData {
//     xacNhanMatKhau: string
// }



// // Gọi API đăng ký
// export async function register(
//     data: RegisterData
// ): Promise<void> {
//     const res = await fetch(
//         'http://localhost:5196/api/Auth/Register',
//         {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify({
//                 tenTK: data.tenTK,
//                 matKhau: data.matKhau,
//                 tenHienThi: data.tenHienThi,
//                 email: data.email,
//                 phone: data.phone
//             }),
//         }
//     );
//     const json: RegisterResponse = await res.json();
//     if (!res.ok || !json.success)
//         throw new Error(json.message || 'Register failed');

// }



export interface loginRequest {
    tenTK: string,
    matKhau: string
}

export const login = async (data: loginRequest) => {
    const res = await axiosClient.post('Auth/Login', data)
    return res
}


export interface RegisterData {
    tenTK: string,
    tenHienThi: string,
    email: string,
    phone: string,
    matKhau: string,
}


export interface RegisterFormData extends RegisterData {
    xacNhanMatKhau: string,
}

export const register = async (data: RegisterData) => {
    const res = axiosClient.post('/Auth/Register', data)
    return res;
}