import {DispatchAction, InitState, rootReducer} from './root-reducer';
import {createStore} from 'redux';

export const store = createStore<InitState, DispatchAction, null, null>(rootReducer);