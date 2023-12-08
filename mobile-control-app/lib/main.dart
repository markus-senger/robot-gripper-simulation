import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'firebase_options.dart';
import 'package:cloud_firestore/cloud_firestore.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );

  runApp(const MobileHMI());
}

class MobileHMI extends StatelessWidget {
  const MobileHMI({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: MainPage(firestore: FirebaseFirestore.instance),
    );
  }
}

class MainPage extends StatelessWidget {
  final FirebaseFirestore _firestore;

  const MainPage({Key? key, required FirebaseFirestore firestore})
      : _firestore = firestore,
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Control Panel - Robotic Gripper', style: TextStyle(color: Colors.white),),
        backgroundColor: const Color.fromRGBO(50, 50, 50, 1.0),
        centerTitle: true,
      ),
      backgroundColor: const Color.fromRGBO(30, 30, 30, 1.0),
      body: Container(
        alignment: Alignment.center,
        decoration: BoxDecoration(
          color: const Color.fromRGBO(50, 50, 50, 1.0),
          borderRadius: BorderRadius.circular(30.0),
        ),
        margin: const EdgeInsets.only(left: 20.0, right: 20.0, top: 20, bottom: 20),
        child: Container(
          width: double.infinity,
          alignment: Alignment.center,
          margin: const EdgeInsets.all(20.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: <Widget>[
              StreamBuilder<DocumentSnapshot>(
                stream: _firestore.collection('RoboticGripper').doc('c33p54088NhwVib4yqU4').snapshots(),
                builder: (context, snapshot) {
                  if (!snapshot.hasData) {
                    return const CircularProgressIndicator(); // Loading indicator while data is fetched
                  }
                  bool isOpen = snapshot.data!['is_open'];
                  bool isClose = snapshot.data!['is_close'];
                  bool isUp = snapshot.data!['is_up'];
                  bool isDown = snapshot.data!['is_down'];
                  bool isPos1 = snapshot.data!['is_pos1'];
                  bool isPos2 = snapshot.data!['is_pos2'];
                  const double entrySize = 9.0;
                  return Column(
                    children: [
                      Container(
                        decoration: BoxDecoration(
                          color: const Color.fromRGBO(60, 60, 60, 1.0),
                          borderRadius: BorderRadius.circular(30.0),
                        ),
                        child: Column(
                          children: [
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 20.0, right: 20.0, top: 20, bottom: 20),
                              width: double.infinity,
                              alignment: Alignment.centerLeft,
                              child: const Text(
                                'Gripper',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 20.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isOpen ? Colors.lightGreen : Colors
                                    .redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is open',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: !isClose && !isOpen
                                    ? Colors.lightGreen
                                    : Colors.redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'in intermediate pos',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isClose ? Colors.lightGreen : Colors
                                    .redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is closed',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      const SizedBox(height: 20.0),
                      Container(
                        decoration: BoxDecoration(
                          color: const Color.fromRGBO(60, 60, 60, 1.0),
                          borderRadius: BorderRadius.circular(30.0),
                        ),
                        child: Column(
                          children: [
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 20.0, right: 20.0, top: 20, bottom: 20),
                              width: double.infinity,
                              alignment: Alignment.centerLeft,
                              child: const Text(
                                'Robotic Position',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 20.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isUp ? Colors.lightGreen : Colors.redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is up',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isDown ? Colors.lightGreen : Colors.redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is down',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isPos1 ? Colors.lightGreen : Colors.redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is on Pos 1',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                            Container(
                              margin: const EdgeInsets.only(
                                  left: 24, right: 24, bottom: 16),
                              width: double.infinity,
                              decoration: BoxDecoration(
                                color: isPos2 ? Colors.lightGreen : Colors.redAccent,
                                borderRadius: BorderRadius.circular(10.0),
                              ),
                              padding: const EdgeInsets.all(entrySize),
                              alignment: Alignment.center,
                              child: const Text(
                                'is on Pos 2',
                                style: TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 16.0,
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  );
                }),
            ]
          ),
        ),
      ),
    );
  }
}
