import { AxiosError } from "axios";

export interface AppError {
  message: string;
  statusCode: number;
  errors?: any;
}

export default function handleApiError(error: unknown): AppError {
  const axiosError = error as AxiosError;
  const statusCode = axiosError.response?.status || -1;
  const message =
    (axiosError.response?.data as any | null)?.message ??
    "An error occurred while processing the request.";
  const errors = (axiosError.response?.data as any | null)?.errors ?? null;

  return {
    message: message,
    statusCode: statusCode,
    errors: errors,
  };
}
