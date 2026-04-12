"use client";

import { useState, useEffect, useRef } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useRouter } from "next/navigation";
import {
  getAllMessagesAsync,
  Message,
  sendMessageAsync,
} from "@/server_actions/messageAction";
import { getMeAsync, User } from "@/server_actions/userAction";
import { ErrorResponse } from "@/server_actions/ServerActionResult";
import createChatHubConnection from "@/signalr/signalRConnection";
import { HubConnection } from "@microsoft/signalr";
import { signOutAsync } from "@/server_actions/authenticationActions";

export default function Chat() {
  const [messages, setMessages] = useState<Message[]>([]);
  const [newMessage, setNewMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const [currentUser, setCurrentUser] = useState<User | null>(null);

  const router = useRouter();
  const messagesEndRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const authAndFetch = async function () {
      try {
        const me = await getMeAsync();
        if (!me.isSuccess || me.response == null) {
          console.error("Current User is null");
          router.replace("/sign-in");
          return;
        }

        setCurrentUser(me.response as User);
        const res = await getAllMessagesAsync();
        if (res.isSuccess) {
          const response = res.response as { messages: Message[] };
          setMessages(response.messages);
        } else {
          const error = res.response as ErrorResponse;
          if (error.statusCode == 401) {
            router.replace("/sign-in");
          } else {
            console.error(error.message, error);
          }
        }
      } catch (error: unknown) {
        console.error("Unknown Error", error);
      }
    };

    authAndFetch();
  }, []);

  useEffect(function () {
    let chatHubConnection: HubConnection;

    const connectToChatHub = async function () {
      try {
        chatHubConnection = createChatHubConnection();

        chatHubConnection.start();
        chatHubConnection.on("ReceiveMessage", function (message: Message) {
          setMessages((prev) => [...prev, message]);
        });

        return chatHubConnection;
      } catch (error) {
        console.error("Failed to connect Chat hub");
      }
    };

    connectToChatHub();

    return function () {
      chatHubConnection.stop();
    };
  }, []);

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

  const handleLogout = async () => {
    try {
      await signOutAsync();
      router.replace("/sign-in");
    } catch (error) {
      console.error("Log out failed", error);
    }
  };

  return (
    <div className="h-screen flex flex-col bg-gray-50">
      {/* Header */}
      <div className="bg-white border-b px-6 py-4 flex justify-between">
        <h1>Chat Forum</h1>
        <h1 className="font-extrabold">{currentUser?.fullName ?? "No Name"}</h1>
        <Button onClick={handleLogout}>Sign Out</Button>
      </div>

      {/* Messages */}
      <div className="flex-1 overflow-y-auto p-4 space-y-3">
        {messages.map((m) => (
          <div key={m.id} className="bg-white p-3 rounded border">
            <div className="text-sm text-gray-500">{m.sender.fullName}</div>
            <div>{m.content}</div>
            <div className="text-[12px] text-gray-400">
              {new Date(m.createdAt).toLocaleString()}
            </div>
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
