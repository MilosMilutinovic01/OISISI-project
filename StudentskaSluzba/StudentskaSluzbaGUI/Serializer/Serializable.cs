﻿namespace StudentskaSluzbaGUI.Serializer
{
    public interface ISerializable
    {
        string[] ToCSV();

        void FromCSV(string[] values);
    }
}
