import MySQLdb

def get_db_conn():
    db = MySQLdb.connect('localhost', "network_snmp", "NetworkSnmp123456", "network_management", use_unicode=True, charset='utf8')

    return db