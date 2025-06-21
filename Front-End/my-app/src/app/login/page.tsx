
import LoginForm from "./loginForm";
import Link from 'next/link';

export default function Login() {
  return (
    <div className="flex min-h-screen">
      {/* Left side */}
      <div className="w-1/3 bg-white flex items-center justify-center px-10">
        <div className="max-w-md w-full">
          <h2 className="text-4xl font-bold mb-2 text-stone-950">WELCOME BACK</h2>
          <p className="text-gray-500 mb-8">Welcome back! Please enter your details. </p>

          <LoginForm />

          <p className="mt-4 text-sm text-center text-stone-500">
            Don’t have an account?{" "}
            <Link href="/signup" target="blank" className="text-red-500 hover:underline font-bold">Sign up to free!</Link>
          </p>
        </div>
      </div>

      {/* Right side */}
      <div className="w-2/3 bg-gray-100 flex items-center justify-center">
        <img
          src="4.jpg"           // Đặt ảnh vào thư mục public/
          alt="WorkHard BG"
          className="w-full h-full object-cover"
        />
      </div>
    </div>
  );
}
