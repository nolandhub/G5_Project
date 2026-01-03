'use client';
import React from "react";
import { Button } from "@/components/ui/button";

export default function AdminLayout({ children }: { children: React.ReactNode }) {
    return (
        <div>

            <div className="bg-red-200 p-5 min-h-screen">
                <h1 className="text-red-600 text-9xl font-bold mb-4 ">🔒 Khu vực Quản trị (Lỏd)</h1>
                <hr className="mb-4" />
                {children}
            </div>
        </div>
    )
}
