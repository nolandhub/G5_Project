
import SignupForm from '../signup/regisForm';

export default function SignupPage() {
    return (
        <div className="flex min-h-screen items-center justify-center bg-gray-100">
            <div className="w-full max-w-md bg-white p-8 rounded shadow">
                <h2 className="text-2xl font-bold mb-6 text-center">Create new account</h2>
                <SignupForm />
            </div>
        </div>
    );
}