﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResortHotelRev2.Models.DB;
using ResortHotelRev2.Models.ViewModel;

namespace ResortHotelRev2.Models.EntityManager
{
    public class RoomManager
    {

        //Returns list of ALL rooms regardless of availability.
        //Uses SYSRoomsTable
        public List<RoomProfileView> GetAllRooms() 
        {
            List<RoomProfileView> allRooms = new List<RoomProfileView>();
            using (ResortDBEntities db = new ResortDBEntities())
            {
                RoomProfileView RoomsList;
                
                foreach (SYSRoomsTable rm in db.SYSRoomsTables)
                {
                    RoomsList = new RoomProfileView();
                    RoomsList.RoomType = rm.RoomType;
                    RoomsList.RoomId = rm.Id;
                    RoomsList.Occupancy = rm.Occupancy;
                    RoomsList.KingBeds = rm.NumberKingBeds;
                    RoomsList.QueenBeds = rm.NumberQueenBeds;
                    RoomsList.Smoking = rm.Smoking;
                    RoomsList.RoomDescription = rm.Description;
                    RoomsList.Image = rm.Image;

                    allRooms.Add(RoomsList);
                }
                return allRooms;
            }

        } // end GetAllRooms

        public List<RoomProfileView> GetRoomsByReservation(int resId)
        {
            List<RoomProfileView> selectedRooms = new List<RoomProfileView>();
            using (ResortDBEntities db = new ResortDBEntities())
            {

                foreach (var occupiedRoom in db.SYSOccupiedRoomTables)
                {

                    if (occupiedRoom.ReservationID == resId)
                    {
                        RoomProfileView RoomsList;

                        foreach (SYSRoomsTable rm in db.SYSRoomsTables)
                        {
                            if (occupiedRoom.RoomID == rm.Id)
                            {
                                RoomsList = new RoomProfileView();
                                RoomsList.RoomType = rm.RoomType;
                                RoomsList.RoomId = rm.Id;
                                RoomsList.Occupancy = rm.Occupancy;
                                RoomsList.KingBeds = rm.NumberKingBeds;
                                RoomsList.QueenBeds = rm.NumberQueenBeds;
                                RoomsList.Smoking = rm.Smoking;
                                RoomsList.RoomDescription = rm.Description;
                                RoomsList.Image = rm.Image;

                                selectedRooms.Add(RoomsList);
                            }

                        } //end foreach SysRooms
                        
                    } // end if occupiedRoom
                    
                } //end foreach occupiedRoom 

                return selectedRooms;

            } // end using db

        } // end GetRoomsByReservation

        //Find which rooms are available for dates selected
        public RoomDataView GetRoomProfileView(DateTime startDate, DateTime endDate)
        {
            RoomDataView RoomDataView = new RoomDataView();
            List<RoomProfileView> availableRooms = GetAllRooms();
            List<DateTime> datesSelected = new List<DateTime>();

            using (ResortDBEntities db = new ResortDBEntities()) {
                List<SYSOccupiedRoomTable> OccupiedRooms = new List<SYSOccupiedRoomTable>();
                //for each date in date range, 
                while (startDate <= endDate)
                {
                    datesSelected.Add(startDate);
                    startDate = startDate.AddDays(1);
                }
                
                //check each room in all rooms, check occupied table in allRooms for those dates

                SYSOccupiedRoomTable occupiedRoom;
                foreach (SYSOccupiedRoomTable ocRm in db.SYSOccupiedRoomTables)
                {
                    occupiedRoom = new SYSOccupiedRoomTable();
                    occupiedRoom.RoomID = ocRm.RoomID;
                    occupiedRoom.CheckIN = ocRm.CheckIN;
                    occupiedRoom.CheckOUT = ocRm.CheckOUT;

                    OccupiedRooms.Add(occupiedRoom);
                }
                                
                foreach (var date in datesSelected)
                {                    
                    foreach (var room in OccupiedRooms)
                    {                        
                        DateTime tempCheckIn = room.CheckIN;
                        while (tempCheckIn <= room.CheckOUT)
                        {
                            if (date == tempCheckIn)
                            {
                                availableRooms.RemoveAll(rm => rm.RoomId == room.RoomID);
                                break;
                            }

                            else
                            {
                                tempCheckIn = tempCheckIn.AddDays(1);
                            }                            
                        }                        
                    }
                }
            } // end using db

            RoomDataView.RoomProfile = availableRooms;
            return RoomDataView;
        } // end GetRoomProfileView
    } // end RoomManager class
}// end namespace