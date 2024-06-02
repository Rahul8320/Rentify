export const Loading = () => {
  return (
    <div className="h-screen flex justify-center items-center">
      <div className="animate-pulse">
        <div className="bg-indigo-200 rounded-lg h-40 p-5 flex items-center shadow-lg">
          <div className="mx-5 text-4xl animate-spin">⚙️</div>
          <h1 className="text-3xl text-orange-500 font-semibold">
            Loading.... Please wait!
          </h1>
        </div>
      </div>
    </div>
  );
};
