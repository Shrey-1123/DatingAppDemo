import { HttpClient } from '@angular/common/http';
import { CoreEnvironment } from '@angular/compiler/src/compiler_facade_interface';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { MembersService } from './members.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl; 

  constructor(private http: HttpClient, private memberService: MembersService) { }

  // gettting messages for both ends
  getMessages(pageNumber, pageSize, container){
    let params = getPaginationHeaders(pageNumber,pageSize);
    params = params.append('Container',container);
    return getPaginatedResult<Message[]>(this.baseUrl+'messages',params,this.http);
  }

  getMessageThread(username: string)
  {
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/' + username);
  }

  sendMessage(username: string, content: string){
      return this.http.post<Message>(this.baseUrl + 'messages' , {recipientUsername: username , content}); // here if name of property conent is same as of parmaters in method then we dont need to spicfy is like recipientUsername

  }

  deleteMessage(id: Number)
  {
    return this.http.delete(this.baseUrl+'messages/'+id);
  }
}
