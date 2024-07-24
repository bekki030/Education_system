﻿using Modelss;
using System.Text.Json;

namespace Education_system.Systems;

public partial class Center
{
    private List<Mentorlar> mentorlar = new List<Mentorlar>();
    string jsonPathMentor = path + "Mentorlar.json";
    public void AddMentor(string name)
    {
        int id = mentorlar.Count > 0 ? mentorlar.Max(m => m.Id) + 1 : 1;
        mentorlar.Add(new Mentorlar() { Id = id, Name = name });
        string serialized = JsonSerializer.Serialize(mentorlar);
        using (StreamWriter writer = new StreamWriter(jsonPathMentor))
        {
            writer.WriteLine(serialized);
        }
    }
    public void UpdateMentor(int id, string name)
    {
        var mentor = mentorlar.FirstOrDefault(m => m.Id == id);
        if (mentor != null)
        {
            mentor.Name = name;
        }
        else
        {
            Console.WriteLine("Mentor not found");
        }
        string serialized = JsonSerializer.Serialize<List<Mentorlar>>(mentorlar);
        using (StreamWriter sw = new StreamWriter(jsonPathMentor))
        {
            sw.WriteLine(serialized);
        }
    }
    public void DeleteMentor(int id)
    {
        var mentor = mentorlar.FirstOrDefault(x => x.Id == id);
        if (mentor != null)
        {
            mentorlar.Remove(mentor);
            Console.WriteLine("Muvaffaqqiyatli o`chdi");
        }
        else
            Console.WriteLine("Kurs not found");
        string serialized = JsonSerializer.Serialize<List<Mentorlar>>(mentorlar);
        using (StreamWriter sw = new StreamWriter(jsonPathMentor))
        {
            sw.WriteLine(serialized);
        }
    }
    public void ListMentorlar()
    {
        mentorlar = JsonReadMentor();
        foreach (var mentor in mentorlar)
        {
            Console.WriteLine($"Mentor: {mentor.Id}  , Name: {mentor.Name}");
        }
    }
    public List<Mentorlar> JsonReadMentor()
    {
        using (StreamReader reader = new StreamReader(jsonPathMentor))
        {
            string json = reader.ReadToEnd();
            mentorlar = JsonSerializer.Deserialize<List<Mentorlar>>(json);
        }
        return mentorlar;
    }

}
