import { Component,OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder  } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{

  public _hubConnection: HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];
  id='';
  ids: string[]=[];
  nick1='';
  nick1s: string[]=[];

  grpmsg='';
  grpmsgs:string[]=[];

  public sendMessage(): void {
    this._hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .then(() => this.message = '')
      .catch(err => console.error(err));
  }

  public getid():void{
    this._hubConnection
    .invoke('printId',this.nick)
    .catch(err=>console.error(err));
  }

  public join():void{
    this._hubConnection
    .invoke('JoinGroup',this.nick)
    .catch(err=>console.error(err));
    console.log("in join method" + this.nick);
  }

  public sendMessageingrp(): void {
    this._hubConnection
      .invoke('SendMessageToGroups', this.nick, this.grpmsg)
      .then(() => this.grpmsg = '')
      .catch(err => console.error(err));
  }

  public leave():void{
    this._hubConnection
    .invoke('LeaveGroup',this.nick)
    .catch(err=>console.error(err));
    console.log("in leave method" + this.nick);
  }

  ngOnInit(){
  }

  constructor() {
    this.nick = window.prompt('Your name:', '');

    this._hubConnection = new HubConnectionBuilder()
                      .withUrl('http://localhost:5000/chat')
                      .build();

    this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
      console.log("in sendtoall method");
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });

    this._hubConnection.on('printId', (id:string) => {
      console.log("in printid method");
      const text1 = `${id}`;
      console.log(text1);
      this.ids.push(text1);
    });

    this._hubConnection.on('JoinGroup', (nick1: string) => {
      console.log("in joingroup method" + nick1);
      const text1 = `${nick1}`;
      console.log(text1);
      this.nick1s.push(text1);
    });

    this._hubConnection.on('LeaveGroup', (nick: string) => {
      console.log("in LeaveGroup method" + nick);
      const text1 = `${nick}`;
      console.log(text1);
      this.nick1s.push(text1);
    });

    this._hubConnection.on('SendMessageToGroups', (nick: string, receivedMessage: string) => {
      console.log("in sendtogroup method");
      const text = `${nick}: ${receivedMessage}`;
      this.grpmsgs.push(text);
    });



    this._hubConnection
        .start()
        .then(() => {console.log('Connection started!')})
        .catch(err => console.log('Error while establishing connection :('));

        console.log(this._hubConnection);
  }
}
