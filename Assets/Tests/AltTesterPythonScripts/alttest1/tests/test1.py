import unittest
from alttester import *
import time
import loguru
import os
import glob

class MyFirstTest(unittest.TestCase):

    altdriver = None

    logger_object = None

    @classmethod
    def setUpClass(self):
        self.altdriver = AltDriver(enable_logging=False)
        self.clean_up_archive()

    @classmethod
    def tearDownClass(self):
        self.altdriver.stop()

    def clean_up_archive(directory='../archive', max_files=10):
        # 디렉토리 내 모든 파일 가져오기
        files = glob.glob(os.path.join(directory, '*'))
        print(files)
        
        # 파일들을 수정 시간 기준으로 정렬
        files.sort(key=os.path.getmtime)
        
        # 파일 개수 확인
        num_files = len(files)
        
        # 파일이 max_files보다 많으면 오래된 파일 삭제
        if num_files > max_files:
            files_to_delete = num_files - max_files
            for i in range(files_to_delete):
                os.remove(files[i])
                print(f"Deleted: {files[i]}")

    def test_shop_click(self):
        try:
            if(self.logger_object is None):
                self.logger_object = self.altdriver.find_object(By.NAME, "Logger")

            levelUpUI = self.altdriver.find_object(By.ID, "9ab2c789-1ec8-41f1-86d4-e727dfcc9c6b")
            xScale = levelUpUI.get_component_property('UnityEngine.RectTransform','localScale.x',"UnityEngine")
            yScale = levelUpUI.get_component_property('UnityEngine.RectTransform','localScale.y',"UnityEngine")
            zScale = levelUpUI.get_component_property('UnityEngine.RectTransform','localScale.z',"UnityEngine")
        
            if xScale == 1 and yScale == 1 and zScale == 1:
                message = 'xScale==1 and yScale==1 and zScale == 1'
                self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[message])

                item_ids = [
                    '3fce2b64-d937-4791-82ae-0a2d0ce09b5a',
                    '4894f14e-b069-41a3-abe5-8e4fc6b2bba6',
                    'd75bbbf9-3a88-4d33-bfa7-58448a3271aa',
                    '91158516-34c9-4a3e-a290-9a3a0eb79c14',
                    '6934d35f-617f-4a29-8381-536aab3e8f57'
                ]

                for item_id in item_ids:
                    try:
                        item = self.altdriver.find_object(By.ID, item_id, enabled=True)
                        if item is not None:
                            item.click()
                            break 
                    except Exception as e:
                        print(e)
                        # self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])
        except Exception as e:
            self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])
            raise  # 추가적인 예외 처리 필요 시 여기에 추가

    # 시작 메뉴 테스트
    # 명령어 : pytest tests\test1.py -k test_start_menu
    def test_start_menu(self):
        try:
            if(self.logger_object is None):
                self.logger_object = self.altdriver.find_object(By.NAME, "Logger")

            gameStart = self.altdriver.find_object(By.NAME, "GameStart", enabled=True)
            characterUI = self.altdriver.find_object(By.NAME, "Character UI", enabled=True)

            if(gameStart is not None):
                message = 'characterUI is enabled'
                self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[message])
                characterUI.click()
            else:
                message = 'characterUI is not enabled'
                self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[message])

        except Exception as e:
            self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])

    # 재시작 테스트
    # 명령어 : pytest tests\test1.py -k test_retry
    def test_retry(self):
        try:
            if(self.logger_object is None):
                self.logger_object = self.altdriver.find_object(By.NAME, "Logger")
            
            retry = self.altdriver.find_object(By.ID, "ca40226a-939f-42be-a4e1-df7894071c83", enabled=True)
            if(retry is not None):
                retry.click()

        except Exception as e:
            self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])


    # 이동 테스트
    # 명령어 : pytest tests\test1.py -k test_move
    def test_move(self):
        try:
            if(self.logger_object is None):
                self.logger_object = self.altdriver.find_object(By.NAME, "Logger")

            while(True):
                self.altdriver.press_keys([AltKeyCode.UpArrow, AltKeyCode.RightArrow],1,1)
                self.test_shop_click()
                self.altdriver.press_key(AltKeyCode.UpArrow,1,1)
                self.test_shop_click()
                self.altdriver.press_keys([AltKeyCode.UpArrow, AltKeyCode.LeftArrow],1,1)
                self.test_shop_click()
                self.altdriver.press_key(AltKeyCode.LeftArrow,1,1)
                self.test_shop_click()
                self.altdriver.press_keys([AltKeyCode.DownArrow, AltKeyCode.LeftArrow],1,1)
                self.test_shop_click()
                self.altdriver.press_key(AltKeyCode.DownArrow,1,1)
                self.test_shop_click()
                self.altdriver.press_keys([AltKeyCode.DownArrow, AltKeyCode.RightArrow],1,1)
                self.test_shop_click()
                self.altdriver.press_key(AltKeyCode.RightArrow,1,1)
                self.test_shop_click()
        
        except Exception as e:
            self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])


    # 전체 테스트
    # 명령어 : pytest tests\test1.py -k test_play_all
    def test_play_all(self):
        try:
            if(self.logger_object is None):
                self.logger_object = self.altdriver.find_object(By.NAME, "Logger")

            self.test_start_menu()

            self.test_move()
            
        except Exception as e:
            self.logger_object.call_component_method("LoggerScript", "LogMessage", assembly="Assembly-CSharp", parameters=[str(e)])
