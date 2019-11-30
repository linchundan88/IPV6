import re

import re

line = "Cats are smarter than dogs";

# matchObj = re.match(r'dogs', line, re.M | re.I)
# if matchObj:
#     print("match --> matchObj.group() : ", matchObj.group())
# else:
#     print("No match!!")
#



# re.I	使匹配对大小写不敏感
# re.M	多行匹配，影响 ^ 和 $

# matchObj = re.search(r'dogs', line, re.M | re.I)
# if matchObj:
#     print( matchObj.group())
# else:
#     print("No match!!")

# a = re.match('www', 'www.runoob.com').span()
# print(a)
#
# # print(re.match('dog', 'this is a dog ') )
#
#


'''
findall(string[, pos[, endpos]])
参数：

string : 待匹配的字符串。
pos : 可选参数，指定字符串的起始位置，默认为 0。
endpos : 可选参数，指定字符串的结束位置，默认为字符串的长度
'''

# pattern = re.compile(r'\d+')   # 查找数字
# result1 = pattern.findall('2222')
# print(result1)

# valid = re.compile(r'''
#                       (^([0-9A-F]{1,2}[-]){5}([0-9A-F]{1,2})$
#                       |^([0-9A-F]{1,2}[:]){5}([0-9A-F]{1,2})$
#                       |^([0-9A-F]{1,2}[.]){5}([0-9A-F]{1,2})$)
#                       ''',
#                       re.VERBOSE | re.IGNORECASE)
#
# str1 = '2001:250:3003:3133:E5A5:840:D8D6:45A3      19 0017.f294.0678  STALE Vl33'
#
# result1 = valid.findall(str1)
# print(result1)

# s = "http://[ipaddress]/SaveData/127.0.0.1/00-0C-F1-56-98-AD/"
# mac=re.search(r'([0-9A-F]{2}[:-]){5}([0-9A-F]{2})', s, re.I).group()
# print(mac)

s = '2001:250:3003:3133:E5A5:840:D8D6:45A3      19 0017.f294.0678  STALE Vl33'
mac=re.search(r'([0-9A-F]{4}[.]){2}([0-9A-F]{4})', s, re.I)
# .group()
print(mac)


# ip=re.search(r'((2[0-5]|1[0-9]|[0-9])?[0-9]\.){3}((2[0-5]|1[0-9]|[0-9])?[0-9])', s, re.I).group()
# print(ip)

