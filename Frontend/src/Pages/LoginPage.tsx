import { Loading } from "@/Components/Custom/Loading";
import { Button } from "@/Components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/Components/ui/form";
import { Input } from "@/Components/ui/input";
import { useToast } from "@/Components/ui/use-toast";
import { AuthToken, AuthUser } from "@/Models/auth";
import { loginSchema, LoginSchema } from "@/Models/schema";
import authService from "@/Services/auth.service";
import { login, verify } from "@/store/authSlice";
import { RootState, updateApiHits } from "@/store/propertySlice";
import { zodResolver } from "@hookform/resolvers/zod";
import { ToastAction } from "@radix-ui/react-toast";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const [loading, setLoading] = useState<boolean>(false);

  const { toast } = useToast();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const isValidToken = useSelector<RootState, boolean>(
    (state) => state.auth.isValidToken
  );

  const loginForm = useForm<LoginSchema>({
    mode: "all",
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const onSubmit = (values: LoginSchema) => {
    setLoading(true);
    authService
      .login(values)
      .then((data: AuthToken) => {
        dispatch(updateApiHits());
        dispatch(login(data));
        authService.verify(data.token).then((userData: AuthUser) => {
          dispatch(updateApiHits());
          dispatch(verify(userData));
        });
        setLoading(false);
        loginForm.reset;
        navigate("/");
      })
      .catch((err: Error) => {
        dispatch(updateApiHits());
        toast({
          variant: "destructive",
          title: "Uh oh! Something went wrong.",
          description: err.message,
          action: <ToastAction altText="Try again">Try again</ToastAction>,
        });
        setLoading(false);
      });
  };

  useEffect(() => {
    if (isValidToken === true) {
      navigate("/");
    }
  }, []);

  if (loading === true) {
    return <Loading />;
  }

  return (
    <div className="max-w-2xl mx-auto my-32 bg-slate-300 p-10 rounded-lg shadow-md">
      <img
        src="/logo.svg"
        alt="logo"
        className="h-12 w-12 mx-auto rounded-lg"
      />
      <h2 className="text-3xl text-center my-2 font-semibold text-slate-700">
        Login to Rantify
      </h2>
      <Form {...loginForm}>
        <form onSubmit={loginForm.handleSubmit(onSubmit)} className="space-y-4">
          <FormField
            control={loginForm.control}
            name="username"
            render={({ field }) => (
              <FormItem className="mx-10">
                <FormLabel className="text-lg">Username</FormLabel>
                <FormControl>
                  <Input placeholder="Ex. rahuldey" {...field} />
                </FormControl>
                <FormMessage className="text-md font-bold" />
              </FormItem>
            )}
          />
          <FormField
            control={loginForm.control}
            name="password"
            render={({ field }) => (
              <FormItem className="mx-10">
                <FormLabel className="text-lg">Password</FormLabel>
                <FormControl>
                  <Input type="password" placeholder="*******" {...field} />
                </FormControl>
                <FormMessage className="text-md font-bold" />
              </FormItem>
            )}
          />
          <div className="text-center">
            <Button
              type="submit"
              variant="outline"
              disabled={!loginForm.formState.isValid}
            >
              Login
            </Button>
          </div>
        </form>
      </Form>
      <div className="my-2 text-center">
        <p>
          Have not an account?{" "}
          <a href="/register" className="hover:text-blue-700">
            Register here
          </a>
        </p>
      </div>
    </div>
  );
};

export default LoginPage;
