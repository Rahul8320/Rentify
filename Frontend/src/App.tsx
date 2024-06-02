import { Header } from "./Components/Custom/Header";
import { Toaster } from "./Components/ui/toaster";
import HomePage from "./Pages/HomePage";

function App() {
  return (
    <>
      {/* Header */}
      <Header />
      {/* Main */}
      <main>
        <HomePage />
      </main>
      {/* Footer */}
      <footer>Made with 💖 by Rahul Dey</footer>
      <Toaster />
    </>
  );
}

export default App;
