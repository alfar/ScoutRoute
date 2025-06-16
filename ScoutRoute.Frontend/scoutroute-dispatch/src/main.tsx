import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { FronteggProvider } from '@frontegg/react'
import { BrowserRouter } from 'react-router-dom'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const contextOptions = {
  baseUrl: "https://app-relwla431iy5.frontegg.com",
  clientId: "b5a7fe2c-20c5-407e-8773-b36fd03210d5",
  appId: "bdabb20b-d85c-4b9c-a565-7a429730ee09"
};

const authOptions = {
  keepSessionAlive: true // Uncomment this in order to maintain the session alive
};

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <FronteggProvider
      contextOptions={contextOptions}
      hostedLoginBox={true}
      authOptions={authOptions}
    >
      <BrowserRouter>
        <QueryClientProvider client={queryClient}>
          <App />
        </QueryClientProvider>
      </BrowserRouter>
    </FronteggProvider>
  </StrictMode>,
)
