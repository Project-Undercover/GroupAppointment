import { View, Text } from "react-native";
import React, { useMemo } from "react";
// import { useTranslation } from "react-i18next";
import theme from "../../utils/theme";

const TextComponent = ({ children, style, bold, semiBold, mediumBold }) => {
  // const { i18n } = useTranslation();
  const lang = "en";

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
    return lang === "en"
      ? getFontWeight
      : bold
      ? theme.FONTS.secondaryFontBold
      : theme.FONTS.primaryFontRegular;
  }, [lang, bold, semiBold, mediumBold]);
  const combinedStyles = [
    { fontFamily: getFontBasedOnLanguage },
    ...(Array.isArray(style) ? style : [style]),
  ];
  return <Text style={combinedStyles}>{children}</Text>;
};

export default TextComponent;
