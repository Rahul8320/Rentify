import { useDispatch, useSelector } from "react-redux";
import { Avatar, AvatarFallback, AvatarImage } from "../ui/avatar";
import { Button } from "../ui/button";
import { RootState } from "@/store/propertySlice";
import { IoMdLogOut } from "react-icons/io";
import { useNavigate } from "react-router-dom";
import { logout } from "@/store/authSlice";

export const Header = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const isUserLoggedIn = useSelector<RootState, boolean>(
    (state) => state.auth.isValidToken
  );

  const handleLogout = () => {
    dispatch(logout());
    navigate("/");
  };

  return (
    <div className="max-w-7xl mx-auto bg-gray-400 h-16 my-2 rounded-lg px-5 flex items-center">
      {/* Heading Section */}
      <div className="flex justify-center items-center w-3/5">
        <Avatar
          onClick={() => {
            navigate("/");
          }}
          className="hover:cursor-pointer"
        >
          <AvatarImage src="/logo.svg" />
          <AvatarFallback>Rantify</AvatarFallback>
        </Avatar>
        <h1 className="text-purple-950 text-3xl font-medium mx-5">
          <strong
            onClick={() => {
              navigate("/");
            }}
            className="hover:cursor-pointer"
          >
            Rantify
          </strong>
        </h1>
        <h3 className="mx-5 text-purple-800 font-semibold">
          ➖ searching property made easy!
        </h3>
      </div>

      {/* Auth Section */}
      <div className="p-2 w-2/5">
        <div className="w-3/4">
          {isUserLoggedIn ? (
            <div className="text-end">
              <Button
                variant="ghost"
                className="text-slate-700"
                onClick={handleLogout}
              >
                <IoMdLogOut className="mx-1 mt-1" />
                <span> Logout</span>
              </Button>
            </div>
          ) : (
            <div className="text-end">
              <Button
                variant="outline"
                className="mx-3 bg-transparent"
                onClick={() => {
                  navigate("/login");
                }}
              >
                Login
              </Button>
              <Button
                variant="outline"
                className="mx-3 bg-transparent"
                onClick={() => {
                  navigate("/register");
                }}
              >
                Register
              </Button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};
