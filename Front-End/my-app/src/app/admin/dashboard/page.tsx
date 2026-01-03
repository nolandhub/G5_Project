// app/dashboard/page.tsx
"use client";

import { useEffect, useState } from "react";
import axiosClient from "@/services/axiosClient";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import { Pencil, Trash2 } from "lucide-react";

interface User {
    maTK: number;
    tenTK: string;
    tenHienThi: string;
    phone: string;
    email: string;
    isVerified: boolean;
    createAt: string;
}

export default function DashboardPage() {
    const [users, setUsers] = useState<User[]>([]);

    const router = useRouter();

    const handleLogout = () => {
        localStorage.removeItem("token");
        router.push("/login");
        toast.success("Đăng xuất thành công")
    };


    useEffect(() => {
        fetchUsers();
    }, []);

    const fetchUsers = async () => {
        try {
            const res = await axiosClient.get("TaiKhoan/GetAll");
            setUsers(res.data);
        } catch (err) {
            console.error("Lỗi khi lấy danh sách user", err);
        }
    };


    return (
        <div className="min-h-screen bg-gray-100 p-6">


            <h1 className="text-2xl font-bold mb-4">Quản lý Tài khoản người dùng</h1>

            <Button
                variant="outline"
                className="hover:bg-red-100 text-red-500 border-red-500 hover:text-red-600 fixed start-440"
                onClick={handleLogout}
            >
                Đăng xuất
            </Button>


            <div className="bg-white p-4 rounded-xl shadow">
                <h2 className="text-xl font-semibold mb-4">Danh sách người dùng</h2>
                <table className="w-full table-auto">
                    <thead>
                        <tr className="bg-gray-200 text-left">
                            <th className="p-2 text-center">Người dùng</th>
                            <th className="p-2 text-center">Tên tài khoản</th>
                            <th className="p-2 text-center">Email</th>
                            <th className="p-2 text-center">Số điện thoại</th>
                            <th className="p-2 text-center">Trạng thái</th>
                            <th className="p-2 text-center">Ngày tạo</th>
                            <th className="p-2 text-center">Hành động</th>

                        </tr>
                    </thead>
                    <tbody>
                        {users.map((user) => (
                            <tr key={user.maTK} className="border-t">
                                <td className="p-2 text-center">{user.tenTK}</td>
                                <td className="p-2 text-center">{user.tenHienThi}</td>
                                <td className="p-2 text-center">{user.phone}</td>
                                <td className="p-2 text-center">{user.email}</td>
                                <td className={`p-2 text-center ${user.isVerified ? "text-green-500" : "text-red-500"} bg-stone-100 rounded-4xl text-center font-bold text-accent`}>{user.isVerified ? "Đã xác thực" : "Chưa xác thực"}</td>
                                <td className="p-2 text-center "> {new Date(user.createAt).toLocaleDateString("vi-VN")}</td>

                                <td className="p-2 flex gap-2 justify-center">
                                    <Button className="bg-green-600">
                                        <Pencil size={16} />
                                        Sửa
                                    </Button>
                                    <Button className="bg-red-500">
                                        <Trash2 size={16} />
                                        Xóa
                                    </Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
