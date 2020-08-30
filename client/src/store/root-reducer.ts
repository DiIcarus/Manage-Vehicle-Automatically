import { Action, Reducer, Dispatch } from "redux";
interface VehicleId {
  id: string;
  ticket_available: string;
}
export interface InitState {
  name: string;
  token: string;
}
export const initState: InitState = {
  name: "Hson",
  token: "",
};
export interface DispatchAction extends Action<ActionType> {
  payload: Partial<InitState>;
}
export enum ActionType {
  UpdateName,
  UpdateToken,
}
export const rootReducer: Reducer<InitState, DispatchAction> = (
  state: InitState = initState,
  action
) => {
  if (action.type === ActionType.UpdateName) {
    return { ...state, name: action.payload.name || "" };
  } else if (action.type === ActionType.UpdateToken) {
    return { ...state, token: action.payload.token || "" };
  } else {
    return state;
  }
};

export class RootDispatcher {
  private readonly dispatch: Dispatch<DispatchAction>;
  constructor(dispatch: Dispatch<DispatchAction>) {
    this.dispatch = dispatch;
  }
  updateName = (name: string) =>
    this.dispatch({ type: ActionType.UpdateName, payload: { name } });
  updateToken = (token: string) =>
    this.dispatch({ type: ActionType.UpdateToken, payload: { token } });
}
