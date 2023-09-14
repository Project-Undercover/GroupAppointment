import React, { useCallback, useMemo, useRef } from "react";
import { View, Image, StyleSheet } from "react-native";
import BottomSheet, { BottomSheetBackdrop } from "@gorhom/bottom-sheet";
import TextComponent from "../TextComponent";
import theme from "../../../utils/theme";
import Header from "./components/Header";
import ReservationInfo from "../../screens/home/components/ReservationInfo";
import DefaultButton from "../DefaultButton";
import { useTranslation } from "react-i18next";

const SessionBottomSheet = ({
  session,
  userChildren,
  handleShowSheet = () => {},
}) => {
  const { t } = useTranslation();
  const bottomSheetModalRef = useRef(null);

  const snapPoints = useMemo(() => ["50%"], []);

  const handlePresentModalPress = useCallback(() => {
    bottomSheetModalRef.current?.present();
  }, []);

  const handleSheetChanges = useCallback((index) => {}, []);
  const renderBackdrop = useCallback(
    (props) => (
      <BottomSheetBackdrop
        {...props}
        disappearsOnIndex={-1}
        appearsOnIndex={0}
      />
    ),
    []
  );
  console.log(userChildren);
  return (
    <BottomSheet
      backdropComponent={renderBackdrop}
      ref={bottomSheetModalRef}
      index={0}
      snapPoints={snapPoints}
      onClose={handleShowSheet}
      enablePanDownToClose
      onChange={handleSheetChanges}
    >
      <View style={styles.contentContainer}>
        <Header title={session?.title} onClose={handleShowSheet} />
        <View className="flex-row items-center gap-2">
          <Image
            style={styles.image}
            source={require("../../../assets/icons/user.png")}
          />
          <TextComponent style={styles.text}>{"Sabreen arar"}</TextComponent>
        </View>
        <View style={styles.spreator}></View>
        <View className="w-full">
          <ReservationInfo
            instructor={session?.instructor}
            startTime={session?.startDate}
            endTime={session?.endDate}
            children={userChildren}
            showCloseChildIcon={true}
            locationName={session?.locationName}
          />
        </View>
        <View className="items-center w-full my-5">
          <DefaultButton text={t("book_now")} />
        </View>
      </View>
    </BottomSheet>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: "rgba(0, 0, 0, 0.1)",
    justifyContent: "center",
    alignItems: "center",

    ...theme.SHADOW.lightShadow,
  },
  contentContainer: {
    paddingHorizontal: 14,
    paddingTop: 14,
    alignItems: "flex-start",
  },
  text: {
    color: theme.COLORS.black,
    fontSize: 15,
  },
  image: {
    width: 50,
    height: 50,
  },
  spreator: {
    height: 1,
    width: "100%",
    backgroundColor: theme.COLORS.primary,
    marginVertical: 5,
    marginBottom: 10,
  },
});

export default SessionBottomSheet;
