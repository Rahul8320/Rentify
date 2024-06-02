import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { Button } from "@/Components/ui/button";
import { ISP } from "@/Models/isp";
import { Loading } from "@/Components/Custom/Loading";
import ispService from "@/Services/property.service";
import { useToast } from "@/Components/ui/use-toast";
import { updateApiHits } from "@/store/ispSlice";
import { ToastAction } from "@/Components/ui/toast";
import { useParams } from "react-router-dom";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/Components/ui/card";

const DetailsPage = () => {
  const [ispDetails, setIspDetails] = useState<ISP | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  const { toast } = useToast();
  const dispatch = useDispatch();
  const { id } = useParams();

  console.log(id);

  useEffect(() => {
    ispService
      .getIspDetails(id!)
      .then((data: ISP) => {
        setIspDetails(data);
        dispatch(updateApiHits());
        setLoading(false);
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
  }, [id]);

  if (loading) {
    return <Loading />;
  }

  if (loading === false && ispDetails === null) {
    return (
      <Card className="sm:max-w-[425px]">
        <CardHeader>
          <CardTitle>ISP Details</CardTitle>
          <CardDescription>We don't find any isp details.</CardDescription>
        </CardHeader>
      </Card>
    );
  }

  return (
    <Card className="sm:max-w-2xl md:max-w-3xl lg:max-w-5xl mx-auto my-10">
      <CardHeader>
        <CardTitle>
          <h2 className="text-2xl mx-5 text-slate-700">Property Details</h2>
        </CardTitle>
      </CardHeader>
      <CardContent>
        <div className="flex items-center p-2">
          <div className="w-1/3 mx-10 bg-green-50 rounded-xl py-1">
            <img src="https://picsum.photos/200/300" alt={ispDetails?.place} />
          </div>
          <div className="w-2/3 bg-gray-200 rounded-lg border-2 border-gray-500 shadow-md">
            <div className="bg-gray-500 py-1 rounded-md">
              <h1 className="text-3xl font-semibold text-white text-center">
                {ispDetails?.place}
              </h1>
            </div>
            <div className="px-5 py-2">
              <p className="text-lg font-medium my-2">
                <span className="mx-5">
                  <i className="fa fa-wifi"></i>
                </span>
                Price: {ispDetails?.price}
              </p>
            </div>
          </div>
        </div>
      </CardContent>
      <CardFooter>
        <Button type="submit">Save changes</Button>
      </CardFooter>
    </Card>
  );
};

export default DetailsPage;
