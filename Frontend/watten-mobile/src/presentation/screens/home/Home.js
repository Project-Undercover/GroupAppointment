import { StyleSheet, ScrollView, View } from "react-native";
import { useCallback } from "react";
import Header from "./components/Header";
import moment from "moment";
import WelcomeBanner from "./components/WelcomeBanner";
import StatCard from "./components/StatCard";
import Spacer from "../../components/Spacer";
import ReservationsList from "./components/ReservationsList";
import { useTranslation } from "react-i18next";
import { useDispatch, useSelector } from "react-redux";
import { useFocusEffect } from "@react-navigation/native";
import UserActions from "../../../actions/UserActions";

const Home = ({ navigation }) => {
  const { t } = useTranslation();
  const { userHomeData } = useSelector((state) => state.users);
  const { user } = useSelector((state) => state.auth);
  const dispatch = useDispatch();
  const userActions = UserActions();

  useFocusEffect(
    useCallback(() => {
      const startDate = moment()
        .locale("he")
        .startOf("day")
        ?.format("yyyy-MM-DDTHH:mm:ssZ");
      dispatch(userActions.fetchUserHomeData(startDate));
    }, [])
  );
  return (
    <View className="flex-1">
      <Header navigation={navigation} />
      <ScrollView
        style={{ flex: 1 }}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{ paddingBottom: 120 }}
      >
        <WelcomeBanner
          username={user?.firstName}
          date={moment().format("LL")}
        />
        <View className="p-4 flex-row justify-around">
          <StatCard
            value={userHomeData?.sessionsCount}
            label={t("done_sessions")}
            iconPath={require("../../../assets/icons/koala.png")}
          />
          <Spacer space={15} />
          <StatCard
            value={userHomeData?.childrenCount}
            label={t("registered_children")}
            iconPath={require("../../../assets/icons/sloth.png")}
          />
        </View>
        <Spacer space={10} />
        <ReservationsList total={3} data={userHomeData?.sessions} />
      </ScrollView>
    </View>
  );
};

export default Home;

const styles = StyleSheet.create({});
