"use client";

import { Dispatch, SetStateAction, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import handleApiError from "@/errors/AppError";
import Link from "next/link";
import { signUpAsync } from "@/server_actions/authenticationActions";
import { ErrorResponse } from "@/server_actions/ServerActionResult";
import { Toaster } from "@/components/ui/sonner";
import { toast } from "sonner";
import { useRouter } from "next/navigation";

export default function SignUp() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [fullname, setFullname] = useState("");
  const [error, setError] = useState("");
  const [isLoading, setLoading]: [boolean, Dispatch<SetStateAction<boolean>>] =
    useState(false);

  const [badRequestErrors, setBadRequestError]: [
    any,
    Dispatch<SetStateAction<any>>,
  ] = useState({});

  const router = useRouter();

  const handleSignUp = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setBadRequestError({});

    try {
      setLoading(true);
      const res = await signUpAsync({
        username: username,
        password: password,
        fullName: fullname,
      });

      if (res.isSuccess) {
        toast("Sign up successfully");
        router.push("/sign-in");
      } else {
        const error: ErrorResponse = res.response as ErrorResponse;
        setBadRequestError(error.errors);
      }
    } catch (error: unknown) {
      console.error("Sign-up failed:", error);
      setError("Sign-up failed");
    } finally {
      setLoading(false);
    }

    const users = JSON.parse(localStorage.getItem("users") || "[]");
    const existingUser = users.find((u: any) => u.username === username);

    if (existingUser) {
      setError("Username already exists");
      return;
    }

    const newUser = { username, password, fullname };
    users.push(newUser);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-sm">
        <h1 className="text-3xl mb-8 text-center">Sign Up</h1>
        <form onSubmit={handleSignUp} className="space-y-6">
          <div className="space-y-2">
            <Label htmlFor="username">Username</Label>
            <Input
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            {(badRequestErrors?.username || badRequestErrors?.Username) && (
              <p className="text-red-500 text-sm">
                {(
                  badRequestErrors?.username ||
                  (badRequestErrors?.Username as [])
                ).join(", ")}
              </p>
            )}
          </div>
          <div className="space-y-2">
            <Label htmlFor="password">Password</Label>
            <Input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />

            {(badRequestErrors?.password || badRequestErrors?.Password) && (
              <p className="text-red-500 text-sm">
                {(
                  badRequestErrors?.password ||
                  (badRequestErrors?.Password as [])
                ).join(", ")}
              </p>
            )}
          </div>
          <div className="space-y-2">
            <Label htmlFor="fullname">Full Name</Label>
            <Input
              id="fullname"
              type="text"
              value={fullname}
              onChange={(e) => setFullname(e.target.value)}
            />

            {(badRequestErrors?.fullName || badRequestErrors?.FullName) && (
              <p className="text-red-500 text-sm">
                {(
                  badRequestErrors?.fullName ||
                  (badRequestErrors?.FullName as [])
                ).join(", ")}
              </p>
            )}
          </div>

          <Button
            type="submit"
            className={`w-full ${isLoading ? "opacity-50 cursor-not-allowed" : ""}`}
          >
            Sign Up
          </Button>
        </form>
        <p className="mt-6 text-center text-sm text-gray-600">
          Already have an account?{" "}
          <Link href="/sign-in" className="text-blue-600 hover:underline">
            Sign In
          </Link>
        </p>
      </div>
    </div>
  );
}
