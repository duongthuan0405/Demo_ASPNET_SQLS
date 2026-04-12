"use client";

import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useRouter } from "next/navigation";
import handleApiError from "@/errors/AppError";
import {
  getAllMessagesAsync,
  Message,
  sendMessageAsync,
} from "@/server_actions/messageAction";
import { getMe } from "@/server_actions/userAction";

export default function Chat() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [newMessage, setNewMessage] = useState("");
  const [loading, setLoading] = useState(false);

  const router = useRouter();
  const messagesEndRef = useRef<HTMLDivElement>(null);

  const token =
    typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    const authAndFetch = async function () {
      try {
        const me = await getMe();
        if (me == null) {
          console.error("Current User is null");
          router.replace("/sign-in");
        }

        const messagesRes = await getAllMessagesAsync();
        setMessages(messagesRes.messages);
      } catch (error: unknown) {
        const appError = handleApiError(error);
        if (appError.statusCode == 401 || appError.statusCode == 403) {
          router.replace("/sign-in");
          console.error(appError);
        }
      }
    };

    authAndFetch();
  }, []);

  // 🔥 Scroll bottom
  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const handleSendMessage = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newMessage.trim()) return;

    try {
      setLoading(true);

      const res = await sendMessageAsync({
        content: newMessage,
      });

      setNewMessage("");
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    router.replace("/sign-in");
  };

  return (
    <div className="h-screen flex flex-col bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b px-6 py-4 flex justify-between">
        <h1>Chat Forum</h1>
        <Button onClick={handleLogout}>Logout</Button>
      </div>

      {/* Messages */}
      <div className="flex-1 overflow-y-auto p-4 space-y-3">
        {messages.map((m) => (
          <div key={m.id} className="bg-white p-3 rounded border">
            <div className="text-sm text-gray-500">{m.sender.fullName}</div>
            <div>{m.content}</div>
          </div>
        ))}
        <div ref={messagesEndRef} />
      </div>

      {/* Input */}
      <div className="p-4 border-t bg-white">
        <form onSubmit={handleSendMessage} className="flex gap-2">
          <Input
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            placeholder="Type message..."
          />
          <Button disabled={loading}>Send</Button>
        </form>
      </div>
    </div>
  );
}
