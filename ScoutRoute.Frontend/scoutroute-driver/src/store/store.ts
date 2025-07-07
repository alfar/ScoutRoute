import { configureStore } from '@reduxjs/toolkit'
import routeReducer from './routeSlice'
import teamReducer from './teamSlice'
// ...

export const store = configureStore({
  reducer: {
    routes: routeReducer,
    team: teamReducer
  }
})

// Infer the `RootState`,  `AppDispatch`, and `AppStore` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
export type AppStore = typeof store