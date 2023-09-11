const theme = {
  COLORS: {
    primary: "#A8B3B9",
    secondaryPrimary: "#1D2C3B",
    secondaryPrimaryLight: "#252f47",
    primaryLight: "#a7bdcd4a",
    secondary: "#FFA3BE",
    secondary2: "#D6DEE7",
    primaryGreen: "#3B9795",
    primaryOrange: "#FF864B",
    primaryOrange2: "#FF6729",
    lightGreen: "rgba(0, 184, 148, 0.10)",

    red: "#F1395E",
    redAlpha: "#f1395e2c",
    black: "#1E1F20",
    white: "#FFFFFF",
    green: "#00B894",
    green2: "#00A344",

    // status
    statusOpen: "#35d0ba",
    statusBusy: "#ff9234",
    statusClosed: "#d92027",

    lightGray: "#F5F5F6",
    lightGrayF: "#FCFCFF",
    lightGray2: "#F6F6F7",
    lightGray3: "#EFEFF1",
    lightGray4: "#BBBBBB",
    gray1: "#C0C0C0",
    gray2: "#999999",
    gray4: "#A3A3A3",
    gray400: "#71717a",
    transparent: "transparent",
    darkgray: "#898C95",
  },

  FONTS: {
    secondaryFontBold: "Cairo-Bold",
    secondaryFontRegular: "Cairo-Regular",

    primaryFontBold: "Rubik-Bold",
    primaryFontMedium: "Rubik-Medium",
    primaryFontSemibold: "Rubik-SemiBold",
    primaryFontRegular: "Rubik-Regular",
  },

  SHADOW: {
    lightShadow: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 1,
      },
      shadowOpacity: 0.2,
      shadowRadius: 1.41,
    },

    orangeShadow: {
      shadowColor: "#FF6729",
      shadowOffset: {
        width: 0,
        height: 3,
      },
      shadowOpacity: 0.27,
      shadowRadius: 4.65,

      elevation: 6,
    },
    btnShadowDark: {
      shadowColor: "#FF6729",
      shadowOpacity: 0.7,
      shadowOffset: {
        height: 4,
        width: 4,
      },
      shadowRadius: 5,
      elevation: 6,
    },
    shadowComponent: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      elevation: 2,
    },
    shadowComponentBottom: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.23,
      shadowRadius: 2.62,

      elevation: 4,
    },
    shadowComponent2: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      elevation: 5,
    },
    shadowComponent3: {
      ...Platform.select({
        ios: {
          shadowOffset: { width: 8, height: 8 },
          shadowColor: "rgba(0, 0, 0, 0.12)",
          shadowOpacity: 1,
          shadowRadius: 45,
        },
        android: {
          elevation: 8,
        },
      }),
    },
    shadowGreen: {
      shadowColor: "#3B9795",
      shadowOffset: {
        width: 0,
        height: 3,
      },
      shadowOpacity: 0.29,
      shadowRadius: 4.65,

      elevation: 7,
    },
    shadowBar: {
      shadowColor: "#A8B3B9",
      shadowOffset: {
        width: 0,
        height: -1,
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      elevation: 5,
    },
    navItem: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: -1,
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      elevation: 5,
    },
  },

  ORDER_STATIS: {
    new_order: {
      main: "#16A34A",
      text: "white",
      description: "new_order",
      next: "accpet_order",
    },
    take_order: {
      2: {
        main: "#fb923c",
        text: "white",
        description: "preparing_order",
        next: "pick_up_order",
      },
      3: {
        main: "#ec4899",
        text: "white",
        description: "order_ready",
        next: "pick_up_order",
      },
    },
    delivered: {
      main: "#0c4a6e",
      text: "white",
      description: "order_picked",
      next: "complete_order",
    },
    order_completed: {
      main: "#A3A3A3",
      text: "white",
      description: "delivered_to_customer",
      next: "delivered_to_customer",
    },
  },
};

export default theme;
