

'use client';
import { useRouter } from 'next/navigation';




export default function Login() {
    const router = useRouter();

    return (

        <div className='justify-items-center space-y-30 bg-yellow-300  h-screen w-screen'>
            <div className=" text-green-500 justify-items-center font-extrabold text-9xl pt-80">
                Welcome to Home!
            </div>
            <div className='flex space-x-2'>
                <button
                    onClick={() => router.push('/login')}
                    className="w-auto bg-blue-500 text-white py-2 rounded hover:bg-blue-600 transition p-2"
                >
                    ← Back
                </button>

                <button
                    onClick={() => router.push('/login')}
                    className="w-auto bg-blue-500 text-white py-2 rounded hover:bg-blue-600 transition p-2"
                >
                    Next →
                </button>
            </div>
        </div>


    );
}
