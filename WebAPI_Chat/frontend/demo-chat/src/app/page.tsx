import { getMeAsync } from "@/server_actions/userAction";
import { redirect } from "next/navigation";

export default async function Home() {
  let isSuccess: boolean = false;
  try {
    const res = await getMeAsync();
    isSuccess = res.isSuccess;
  } catch (error) {
    isSuccess = false;
  } finally {
    if (isSuccess) {
      redirect("/chat");
    } else {
      redirect("/sign-in");
    }
  }
}
