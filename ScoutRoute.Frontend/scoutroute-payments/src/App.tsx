import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import './App.css';
import RegisterPaymentForm from './forms/RegisterPaymentForm';

const queryClient = new QueryClient();

function App() {

  return (
    <QueryClientProvider client={queryClient}>
      <nav>
        <ul>
          <li><a href="/payments">Indbetalinger</a></li>
        </ul>
      </nav>
      <main>
        <RegisterPaymentForm />
      </main>
    </QueryClientProvider>
  )
}

export default App
