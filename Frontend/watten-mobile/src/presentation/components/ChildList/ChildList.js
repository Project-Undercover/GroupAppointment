import { useEffect, useState } from "react";
import { ScrollView } from "react-native-gesture-handler";
import ChildCard from "./components/ChildCard";

const ChildList = ({ data, showCloseChildIcon, handleRemoveChild }) => {
  return (
    <ScrollView
      horizontal={true}
      style={{ flex: 1, width: "100%" }}
      showsHorizontalScrollIndicator={false}
    >
      {data?.map((child) => {
        return (
          <ChildCard
            value={child?.name}
            key={child?.id}
            showCloseChildIcon={showCloseChildIcon}
            handleRemoveChild={() => handleRemoveChild(child?.id)}
          />
        );
      })}
    </ScrollView>
  );
};

export default ChildList;
