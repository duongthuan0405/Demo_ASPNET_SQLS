"use server";

import axiosClient from "@/api/axiosClient";
import ServerActionResult from "./ServerActionResult";
import handleApiError, { AppError } from "@/errors/AppError";

export interface Message {
  id: string;
  content: string;
  senderId: string;
  createdAt: string;
  lastUpdatedAt: string;
  sender: {
    id: string;
    username: string;
    fullName: string;
  };
}

export const getAllMessagesAsync = async (): Promise<
  ServerActionResult<{
    messages: Message[];
  }>
> => {
  try {
    const res: {
      messages: Message[];
    } = await axiosClient.get("/api/messages");
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
};

export const sendMessageAsync = async (params: {
  content: string;
}): Promise<ServerActionResult<{ id: string }>> => {
  try {
    const res: { id: string } = await axiosClient.post("/api/messages", params);

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
};
