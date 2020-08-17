import {Action, Reducer, Dispatch} from "redux"
export interface InitState{
  name:string
}
export const initState: InitState = {
  name:'Hson'
}
export interface DispatchAction extends Action<ActionType> {
  payload: Partial<InitState>
}
export enum ActionType{
  UpdateName,
}
export const rootReducer:Reducer<InitState,DispatchAction> = (state:InitState = initState,action) => {
  if(action.type === ActionType.UpdateName){
    return {...state, name: action.payload.name || ''}
  }else{
    return state
  }
}

export class RootDispatcher{
  private readonly dispatch: Dispatch<DispatchAction> ;
  constructor(dispatch: Dispatch<DispatchAction>){
    this.dispatch = dispatch;
  }
  updateName = (name:string) => this.dispatch({type: ActionType.UpdateName, payload:{name}});
}