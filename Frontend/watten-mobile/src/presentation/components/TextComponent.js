import { View, Text } from "react-native";
import React, { useMemo } from "react";
import theme from "../../utils/theme";
import i18next from "i18next";

const TextComponent = ({ children, style, bold, semiBold, mediumBold }) => {
  const getFontWeight = useMemo(() => {
    if (bold) {
      return theme.FONTS.primaryFontBold;
    }
    if (semiBold) {
      return theme.FONTS.primaryFontSemibold;
    }
    if (mediumBold) {
      return theme.FONTS.primaryFontMedium;
    }
    return theme.FONTS.primaryFontRegular;
  }, [bold, semiBold, mediumBold]);

  const getFontBasedOnLanguage = useMemo(() => {
    return i18next.language === "he"
      ? getFontWeight
      : bold || semiBold || mediumBold
      ? theme.FONTS.secondaryFontBold
      : theme.FONTS.primaryFontRegular;
  }, [i18next.language, bold, semiBold, mediumBold]);
  const combinedStyles = [
    { fontFamily: getFontBasedOnLanguage, textAlign: "left" },
    ...(Array.isArray(style) ? style : [style]),
  ];
  return <Text style={combinedStyles}>{children}</Text>;
};

export default TextComponent;
