"use client";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { signInAsync } from "@/server_actions/authenticationActions";
import { ErrorResponse } from "@/server_actions/ServerActionResult";
import { getMeAsync } from "@/server_actions/userAction";

export default function SignIn() {
  const [username, setUsername]: [
    string,
    React.Dispatch<React.SetStateAction<string>>,
  ] = useState("");

  const [password, setPassword]: [
    string,
    React.Dispatch<React.SetStateAction<string>>,
  ] = useState("");

  const [error, setError]: [
    string,
    React.Dispatch<React.SetStateAction<string>>,
  ] = useState("");

  const [loading, setLoading]: [
    boolean,
    React.Dispatch<React.SetStateAction<boolean>>,
  ] = useState(false);

  const [badRequestErrors, setBadRequestErrors]: [
    any,
    Dispatch<SetStateAction<any>>,
  ] = useState({});

  const router = useRouter();

  const handleSignIn = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      setBadRequestErrors({});
      setError("");

      setLoading(true);
      let res = await signInAsync({
        username: username,
        password: password,
      });
      if (res.isSuccess) {
        router.replace("/chat");
      } else {
        const error: ErrorResponse = res.response as ErrorResponse;
        setBadRequestErrors(error.errors);
        setError(error.message);
      }
    } catch (error) {
      console.log("Sign-in failed:", error);
      setError("Sign-in failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50">
      <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-sm">
        <h1 className="text-3xl mb-8 text-center">Sign In</h1>
        <form onSubmit={handleSignIn} className="space-y-6">
          <div className="space-y-2">
            <Label htmlFor="username">Username</Label>
            <Input
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            {(badRequestErrors?.Username || badRequestErrors?.username) && (
              <p className="text-red-500 text-sm">
                {badRequestErrors?.Username || badRequestErrors?.username}
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
            {(badRequestErrors?.Password || badRequestErrors?.password) && (
              <p className="text-red-500 text-sm">
                {badRequestErrors?.Password || badRequestErrors?.password}
              </p>
            )}
          </div>
          {error && <p className="text-red-500 text-sm">{error}</p>}
          <Button
            type="submit"
            className={`w-full ${loading ? "opacity-50 cursor-not-allowed" : ""}`}
            disabled={loading}
          >
            Sign In
          </Button>
        </form>
        <p className="mt-6 text-center text-sm text-gray-600">
          Don't have an account?{" "}
          <Link href="/sign-up" className="text-blue-600 hover:underline">
            Sign Up
          </Link>
        </p>
      </div>
    </div>
  );
}
