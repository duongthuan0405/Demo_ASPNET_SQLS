"use server";
import axiosClient from "@/api/axiosClient";
import handleApiError, { AppError } from "@/errors/AppError";
import ServerActionResult from "./ServerActionResult";

export interface User {
  id: string;
  fullName: string;
  username: string;
}

export async function getMe(): Promise<ServerActionResult<User>> {
  try {
    const res: User = await axiosClient.get("/api/users/me");

    return {
      isSuccess: true,
      response: res,
    };
  } catch (error) {
    const appError: AppError = handleApiError(error);
    return {
      isSuccess: false,
      response: appError,
    };
  }
}
