// src/services/authService.ts



export interface LoginResponse {
    success: boolean;
    message: string;
    data: LoginData;
}

export interface LoginData {
    maTK: number;
    tenHienThi: string;
    token: string;
    expireIn: number;
}


export async function login(
    tenTK: string,
    matKhau: string
): Promise<LoginData> {
    const res = await fetch(
        'https://g5-project.onrender.com/api/Auth/Login',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ tenTK, matKhau }),
        }
    );

    const json: LoginResponse = await res.json();
    if (!res.ok || !json.success)
        throw new Error(json.message || 'Login failed');

    return json.data;
}


export interface RegisterResponse {

    success: boolean;
    message: string;
}


export interface RegisterData {
    tenTK: string,
    matKhau: string,
    tenHienThi: string,
    email: string,
    phone: string,
}



// Gọi API đăng ký
export async function register(
    data: RegisterData
): Promise<void> {
    const res = await fetch(
        'https://g5-project.onrender.com/api/Auth/Register',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                tenTK: data.tenTK,
                matKhau: data.matKhau,
                tenHienThi: data.tenHienThi,
                email: data.email,
                phone: data.phone
            }),
        }
    );
    const json: RegisterResponse = await res.json();
    if (!res.ok || !json.success)
        throw new Error(json.message || 'Register failed');

}

