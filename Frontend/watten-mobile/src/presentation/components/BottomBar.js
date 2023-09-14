import React, { useState, useEffect } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  Animated,
  LayoutAnimation,
  UIManager,
} from "react-native";
import { Entypo, Feather, Ionicons } from "@expo/vector-icons";

import { useNavigation } from "@react-navigation/native";
import theme from "../../utils/theme";
import TextComponent from "./TextComponent";
import { useTranslation } from "react-i18next";

if (Platform.OS === "android") {
  UIManager.setLayoutAnimationEnabledExperimental(true);
}

const BottomBar = () => {
  const [activeTab, setActiveTab] = useState("home");
  const [animationActive, setAnimationActive] = useState(false);
  const navigation = useNavigation();
  const [fadeAnimation] = useState(new Animated.Value(0));
  const { t } = useTranslation();

  useEffect(() => {
    Animated.timing(fadeAnimation, {
      toValue: 1,
      duration: 300,
      useNativeDriver: true,
    }).start();
  }, [activeTab, fadeAnimation]);

  const handleSelectTab = (tab) => {
    if (activeTab !== tab) {
      LayoutAnimation.configureNext(LayoutAnimation.Presets.easeInEaseOut);
      setAnimationActive(true);
      setActiveTab(tab);
    } else {
      setAnimationActive(false);
    }

    navigation.navigate(tab);
  };

  const tabRoutes = [
    {
      text: t("home"),
      tab: "home",
      icon: (
        <Entypo
          name="home"
          color={
            activeTab === "home"
              ? theme.COLORS.primary
              : theme.COLORS.secondaryPrimary
          }
          size={25}
        />
      ),
    },
    {
      text: t("sessions"),
      tab: "sessionsStack",
      icon: (
        <Ionicons
          name="calendar"
          color={
            activeTab === "sessionsStack"
              ? theme.COLORS.primary
              : theme.COLORS.secondaryPrimary
          }
          size={25}
        />
      ),
    },
    {
      text: t("users"),
      tab: "usersStack",
      icon: (
        <Feather
          name="users"
          color={
            activeTab === "usersStack"
              ? theme.COLORS.primary
              : theme.COLORS.secondaryPrimary
          }
          size={25}
        />
      ),
    },
    {
      text: t("profile"),
      tab: "profile",
      icon: (
        <Feather
          name="user"
          color={
            activeTab === "profile"
              ? theme.COLORS.primary
              : theme.COLORS.secondaryPrimary
          }
          size={25}
        />
      ),
    },
  ];

  return (
    <View style={styles.container}>
      {tabRoutes.map((route, index) => {
        const isActive = activeTab === route.tab;
        const iconOpacity = isActive ? 1 : fadeAnimation;

        return (
          <TouchableOpacity
            style={[styles.tab, isActive && styles.activeTab]}
            onPress={() => handleSelectTab(route.tab)}
            key={index}
            isActive
          >
            <Animated.View
              style={[
                styles.tabAnimation,
                !isActive && { backgroundColor: "white" },
                isActive && animationActive && styles.activeTabAnimation,
                { opacity: iconOpacity },
              ]}
            >
              {route.icon}
              {isActive ? (
                <TextComponent mediumBold style={styles.tabText}>
                  {route.text}
                </TextComponent>
              ) : null}
            </Animated.View>
          </TouchableOpacity>
        );
      })}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    position: "absolute",
    alignSelf: "center",
    bottom: 0,
    backgroundColor: theme.COLORS.white,
    justifyContent: "space-around",
    alignItems: "center",
    alignSelf: "center",
    flexDirection: "row",
    width: "100%",
    height: 75,
    borderStartStartRadius: 25,
    borderStartEndRadius: 25,
    paddingBottom: Platform.select({
      ios: 10,
      android: 10,
    }),
    ...theme.SHADOW.shadowComponentBottom,
  },
  tab: {
    flexDirection: "row",
    alignItems: "center",
  },
  tabAnimation: {
    flexDirection: "row",
    alignItems: "center",
    gap: 4,
    backgroundColor: theme.COLORS.primaryLight,
    borderRadius: 20,
    paddingHorizontal: 15,
    paddingVertical: 5,
  },
  tabText: {
    fontSize: 12,
    marginTop: 4,
    color: theme.COLORS.primary,
  },
  activeTabAnimation: {
    transform: [{ scale: 1.1 }],
  },
  activeTabText: {
    color: theme.COLORS.white,
  },
});

export default BottomBar;
