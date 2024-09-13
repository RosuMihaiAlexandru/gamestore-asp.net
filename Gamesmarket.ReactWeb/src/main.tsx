import { createContext } from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import { RootStore } from "./store/RootStore.ts";
import { BrowserRouter } from "react-router-dom";

interface State {
  rootStore: RootStore;
}

export const rootStore = new RootStore();

export const Context = createContext<State>({
  rootStore,
});

ReactDOM.createRoot(document.getElementById("root")!).render(
  <Context.Provider value={{ rootStore }}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Context.Provider>,
);
