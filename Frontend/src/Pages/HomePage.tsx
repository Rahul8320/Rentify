import { useEffect, useState } from "react";
import { columns } from "@/Components/ui/columns";
import { DataTable } from "@/Components/ui/data-table";
import propertyService from "@/Services/property.service";
import { useToast } from "@/Components/ui/use-toast";
import { ToastAction } from "@/Components/ui/toast";
import { Loading } from "@/Components/Custom/Loading";
import { useDispatch } from "react-redux";
import { updateApiHits, updateProperties } from "@/store/propertySlice";
import { PropertyModel } from "@/Models/propertyModel";
import { Property } from "@/Models/property";

const HomePage = () => {
  const [loading, setLoading] = useState<boolean>(true);
  const [properties, setProperties] = useState<PropertyModel[]>([]);

  const { toast } = useToast();
  const dispatch = useDispatch();

  useEffect(() => {
    propertyService
      .getAllProperties()
      .then((data: Property[]) => {
        const propertyModel = Property.toModelList(data);
        setProperties(propertyModel);
        dispatch(updateProperties(data.length));
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
      <DataTable columns={columns} data={properties} />
    </div>
  );
};

export default HomePage;
