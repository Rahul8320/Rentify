import { useSelector } from "react-redux";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { IspState } from "@/store/ispSlice";

export const Header = () => {
  const totalIspCount = useSelector((state: IspState) => state.ispsCount);
  const totalApiHits = useSelector((state: IspState) => state.apiHits);

  return (
    <div className="max-w-7xl mx-auto bg-gray-400 h-16 my-2 rounded-lg px-5 flex items-center">
      {/* Heading Section */}
      <div className="flex justify-center items-center w-3/5">
        <Avatar>
          <AvatarImage src="https://duckduckgo.com/i/b88d18e1.png" />
          <AvatarFallback>Rantify</AvatarFallback>
        </Avatar>
        <h1 className="text-purple-950 text-3xl font-medium mx-5">
          <strong> Rantify </strong>
        </h1>
        <h3 className="mx-5 text-purple-800 font-semibold">
          ➖ searching property made easy!
        </h3>
      </div>

      {/* Count Section */}
      <div className="w-2/5 flex items-center">
        <div className="border-2 border-purple-950 rounded-lg bg-purple-50 p-2 w-2/3 mx-auto text-center">
          <h3 className="text-lg">
            <strong>Total Property</strong>: {totalIspCount}
            <span className="mx-5 text-green-800">|</span>{" "}
            <strong>API Hits</strong>: {totalApiHits}
          </h3>
        </div>
      </div>
    </div>
  );
};
