import { Outlet } from "react-router-dom";
import { Header } from "./Components/Custom/Header";
import { Toaster } from "./Components/ui/toaster";
import Footer from "./Components/Custom/Footer";

function App() {
  return (
    <>
      {/* Header */}
      <Header />
      {/* Main */}
      <main>
        <Outlet />
      </main>
      <Toaster />
      {/* Footer */}
      <Footer />
    </>
  );
}

export default App;
