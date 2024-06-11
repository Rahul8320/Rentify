import { RootState } from "@/store/propertySlice";
import { useSelector } from "react-redux";

const Footer = () => {
  const totalPropertyCount = useSelector<RootState, number>(
    (state) => state.property.propertyCount
  );
  const totalApiHits = useSelector<RootState, number>(
    (state) => state.property.apiHits
  );

  return (
    <footer className="relative overflow-hidden py-3 bg-gray-100 mt-5">
      <section className="text-center text-gray-500">
        <span> Total Property: {totalPropertyCount}</span>
        <span className="mx-3">|</span>
        <span> API Hits: {totalApiHits} </span>
        <span className="mx-3">|</span>
        <span>Rantify &copy; 2024 </span>
        <span className="mx-3">|</span>
        <span>Made with 💖 by Rahul Dey</span>
      </section>
    </footer>
  );
};

export default Footer;
