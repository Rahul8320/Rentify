import { ColumnDef } from "@tanstack/react-table";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";
import { IspModel } from "@/Models/ispModel";
import { Button } from "./button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "./dropdown-menu";
import { Dialog, DialogTrigger } from "./dialog";
import DetailsPage from "@/Pages/DetailsPage";

export const columns: ColumnDef<IspModel>[] = [
  {
    accessorKey: "place",
    header: () => <div className="mx-12">Place</div>,
  },
  {
    accessorKey: "price",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          Price
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      );
    },
    cell: ({ row }) => {
      return <div className="mx-7">{row.getValue("price")}</div>;
    },
  },
  {
    accessorKey: "noOfBedroom",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          NoOfBedroom
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      );
    },
  },
  {
    accessorKey: "sizeinSqft",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          SizeinSqft
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      );
    },
  },
  {
    accessorKey: "lastUpdated",
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
        >
          LastUpdated
          <ArrowUpDown className="ml-2 h-4 w-4" />
        </Button>
      );
    },
  },
  {
    id: "actions",
    cell: ({ row }) => {
      const isp = row.original;

      return (
        <div className="text-right">
          <Dialog>
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="ghost" className="h-8 w-8 p-0">
                  <span className="sr-only">Open menu</span>
                  <MoreHorizontal className="h-4 w-4" />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuLabel>Actions</DropdownMenuLabel>
                <DropdownMenuSeparator />
                <DialogTrigger asChild>
                  <DropdownMenuItem>View details</DropdownMenuItem>
                </DialogTrigger>
                <DropdownMenuItem onClick={async () => await handleShare(isp)}>
                  Share
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
            <DetailsPage id={isp.id} />
          </Dialog>
        </div>
      );
    },
  },
];

const handleShare = async (isp: IspModel) => {
  const shareData = {
    title: `Check out this Place: ${isp.place}`,
    text: `Place: ${isp.place}\nPrice: ${isp.price}\n`,
    url: window.location.href,
  };

  try {
    await navigator.share(shareData);
  } catch (err) {
    console.error("Error sharing:", err);
    alert("Web Share API is not supported in your browser.");
  }
};
