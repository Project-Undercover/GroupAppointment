import React, { useCallback, useEffect, useMemo, useRef } from "react";
import { ScrollView, StyleSheet } from "react-native";
import BottomSheet, { BottomSheetBackdrop } from "@gorhom/bottom-sheet";
import theme from "../../../utils/theme";
import { useDispatch } from "react-redux";
import SessionActions from "../../../actions/SessionActions";
import { useFocusEffect } from "@react-navigation/native";
const ParticipantsBottomSheet = ({ sessionId, handleShowSheet = () => {} }) => {
  const bottomSheetModalRef = useRef(null);
  const sessionActions = SessionActions();
  const dispatch = useDispatch();

  const snapPoints = useMemo(() => ["92%"], []);

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

  useFocusEffect(
    useCallback(() => {
      dispatch(sessionActions.fetchSessionParticipants({ sessionId }));
    }, [])
  );
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
      <ScrollView
        style={styles.contentContainer}
        contentContainerStyle={{
          alignItems: "flex-start",
        }}
      ></ScrollView>
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
  },
});

export default ParticipantsBottomSheet;
