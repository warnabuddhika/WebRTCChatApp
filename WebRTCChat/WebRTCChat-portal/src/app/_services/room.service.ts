import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Room } from '../models/room';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  baseUrl = environment.messageUrl;
  
  constructor(private http: HttpClient) { }

  getRooms(pageNumber, pageSize){
    debugger
    let params = getPaginationHeaders(pageNumber, pageSize);
    debugger
    return getPaginatedResult<Room[]>(this.baseUrl+'room', params, this.http);
    debugger
  }

  addRoom(name: string){
    return this.http.post(this.baseUrl + 'room?name=' + name, {});
  }

  editRoom(id: number, name: string){
    return this.http.put(this.baseUrl + 'room?id='+ id +'&editName='+name, {})
  }

  deleteRoom(id: number){
    return this.http.delete(this.baseUrl+'room/'+id);
  }

  deleteAll(){
    return this.http.delete(this.baseUrl+'room/delete-all');
  }
}
