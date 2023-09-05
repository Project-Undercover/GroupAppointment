import { StyleSheet, ScrollView, View } from "react-native";
import React from "react";
import Header from "./components/Header";
import moment from "moment";
import WelcomeBanner from "./components/WelcomeBanner";
import StatCard from "./components/StatCard";
import Spacer from "../../components/Spacer";
import ReservationsList from "./components/ReservationsList";
const Home = () => {
  return (
    <View className="flex-1">
      <Header />
      <ScrollView
        style={{ flex: 1 }}
        contentContainerStyle={{ paddingBottom: 120 }}
      >
        <WelcomeBanner username={"Sabreen"} date={moment().format("LL")} />
        <View className="p-4 flex-row justify-around">
          <StatCard
            value={10}
            label={"Therapy completed"}
            iconPath={require("../../../assets/icons/koala.png")}
          />
          <Spacer space={15} />
          <StatCard
            value={2}
            label={"Children registered"}
            iconPath={require("../../../assets/icons/sloth.png")}
          />
        </View>
        <Spacer space={10} />
        <ReservationsList total={3} />
      </ScrollView>
    </View>
  );
};

export default Home;

const styles = StyleSheet.create({});
