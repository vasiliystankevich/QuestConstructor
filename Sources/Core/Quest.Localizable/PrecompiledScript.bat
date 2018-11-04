echo "convert %1i18n\common\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\common\messages.txt" "%1i18n\common\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\common\messages.Designer.cs" /publicclass

echo "convert %1i18n\quest\core\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\quest\core\messages.txt" "%1i18n\quest\core\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\quest\core\messages.Designer.cs" /publicclass

echo "convert %1i18n\quest\controls\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\quest\controls\messages.txt" "%1i18n\quest\controls\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\quest\controls\messages.Designer.cs" /publicclass

echo "convert %1i18n\quest\constructor\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\quest\constructor\messages.txt" "%1i18n\quest\constructor\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\quest\constructor\messages.Designer.cs" /publicclass

echo "convert %1i18n\quest\interview\messages.txt"
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe" "%1i18n\quest\interview\messages.txt" "%1i18n\quest\interview\messages.resx" /str:cs,Translations.i18n.common,messages,"%1i18n\quest\interview\messages.Designer.cs" /publicclass