"use server";

import axiosClient from "@/api/axiosClient";
import handleApiError from "@/errors/AppError";
import { cookies } from "next/headers";
import ServerActionResult from "./ServerActionResult";

export const signInAsync = async function (params: {
  username: string;
  password: string;
}): Promise<ServerActionResult<{ token: string }>> {
  try {
    var res = await axiosClient.post<{ token: string }>(
      "/api/authentication/sign-in",
      params,
    );

    (await cookies()).set("token", res.token, {
      httpOnly: true,
      path: "/",
    });

    return {
      isSuccess: true,
      response: res,
    };
  } catch (error) {
    const apiError = handleApiError(error);
    return {
      isSuccess: false,
      response: apiError,
    };
  }
};

export const signUpAsync = async function (params: {
  username: string;
  password: string;
  fullName: string;
}): Promise<ServerActionResult<{ userId: string }>> {
  try {
    const res: { userId: string } = await axiosClient.post(
      "/api/authentication/sign-up",
      params,
    );
    return {
      isSuccess: true,
      response: res,
    };
  } catch (error) {
    const apiError = handleApiError(error);
    return {
      isSuccess: false,
      response: apiError,
    };
  }
};
