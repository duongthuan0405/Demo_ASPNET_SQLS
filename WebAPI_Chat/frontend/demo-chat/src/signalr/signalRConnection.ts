import * as signalR from "@microsoft/signalr";

const chatHubConnection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5225/signalr-hub/chat", {
    accessTokenFactory: () => sessionStorage.getItem("token")!,
  })
  .withAutomaticReconnect();

export default function createChatHubConnection() {
  return chatHubConnection.build();
}
