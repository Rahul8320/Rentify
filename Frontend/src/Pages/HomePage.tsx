import { useEffect, useState } from "react";
import { columns } from "@/Components/ui/columns";
import { DataTable } from "@/Components/ui/data-table";
import { IspModel } from "@/Models/ispModel";
import ispService from "@/Services/property.service";
import { useToast } from "@/Components/ui/use-toast";
import { ToastAction } from "@/Components/ui/toast";
import { Loading } from "@/Components/Custom/Loading";
import { ISP } from "@/Models/isp";
import { useDispatch } from "react-redux";
import { updateApiHits, updateIsps } from "@/store/ispSlice";

const HomePage = () => {
  const [loading, setLoading] = useState<boolean>(true);
  const [isps, setIsps] = useState<IspModel[]>([]);

  const { toast } = useToast();
  const dispatch = useDispatch();

  useEffect(() => {
    ispService
      .getAllIsp()
      .then((data: ISP[]) => {
        const ispModels = ISP.toModelList(data);
        setIsps(ispModels);
        dispatch(updateIsps(data.length));
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
  }, []);

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="max-w-7xl mx-auto">
      <DataTable columns={columns} data={isps} />
    </div>
  );
};

export default HomePage;
