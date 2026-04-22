export default interface ServerActionResult<T> {
  isSuccess: boolean;
  response: T | ErrorResponse;
}

export interface ErrorResponse {
  statusCode: number;
  message: string;
  errors?: any;
}
