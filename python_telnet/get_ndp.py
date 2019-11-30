import re
import paramiko
from helper_db import get_db_conn
import my_config

'''
10.48.2.253 do not have ipv6 address
10.54.2.254  Aruba, password error
10.47.2.254 can not connect  
'''
def get_ipv6():
    client = paramiko.SSHClient()

    db_conn = get_db_conn()
    db_conn.autocommit(True)
    sql = "SELECT * FROM device WHERE get_ndp=1 and device_type like 'CISCO%' or device_type like 'N5548%' "
    cursor = db_conn.cursor()
    cursor.execute(sql)
    results = cursor.fetchall()
    for rs in results:
        ip = rs[1]
        print('get ipv6 address from ip:', ip)

        try:
            client.set_missing_host_key_policy(paramiko.AutoAddPolicy())
            ssh_username = my_config.ssh_username
            ssh_password = my_config.ssh_password
            client.connect(ip, username=ssh_username, password=ssh_password)

            stdin, stdout, stderr = client.exec_command('show ipv6 neighbor')

            for line in stdout:
                match_ip = re.search(
                    r'([\S\s]*[0-9A-F]{4}[:]){2}([0-9A-F]{4})[\S\s]*',
                    line, re.I)

                #first line ip, next line mac
                if match_ip is not None:
                    ip = match_ip.group()
                    ip = ip.split(' ')[0]

                match_mac = re.search(r'([0-9A-F]{4}[.]){2}([0-9A-F]{4})', line, re.I)
                if match_mac is not None:
                    mac = match_mac.group()
                    # print(line)
                    print("MAC:{}, IPV6 Address:{}".format(mac, ip))  # debug only
                    mac = mac.replace('-', '')
                    mac = mac.replace('.', '')
                    mac = mac.replace(':', '')
                    mac = mac.upper()

                    ret1 = cursor.callproc('insert_ARP_new', args=(mac, ip, 3, 0, 0))
        except:
            print('get ipv6 address error, device ip:{}', ip)

        client.close()


get_ipv6()


print('OK')

